﻿var pollingId = null;
const rowCloneId = 'data-file-row-to-clone';

const fileInputElement = document.getElementById('formFileMultiple');
const buttonUploadElement = document.getElementById('btnUpload');
const tableElement = document.getElementById('tblFiles');
const tbodyElement = tableElement.getElementsByTagName("tbody")[0];
const rowCloneElement = document.getElementById(rowCloneId);
const canvasLabelElement = document.getElementById('image-offcanvas-label');
const canvasImageElement = document.getElementById('imgCanvasFile');
const offCanvasElementList = [].slice.call(document.querySelectorAll('.offcanvas'));
const offCanvasList = offCanvasElementList.map((offcanvasEl) => new bootstrap.Offcanvas(offcanvasEl));
const errorAlertElement = bootstrap.Toast.getOrCreateInstance(document.getElementById('divErrorAlert'));

const processStatus = {
    pending: 1,
    processing: 2,
    failed: 3,
    success: 4
};

const configureElements = () => {
    disableUploadButton();
    setImageNotFoundElement();
};

const configureEvents = () => {
    fileInputElement.onchange = (event) => {
        let files = Array.from(event.target.files);
        if (files && files.length > 0) {
            enableUploadButton();
        }
        else {
            disableUploadButton();
        }
    };

    buttonUploadElement.addEventListener("click", () => {
        let files = Array.from(fileInputElement.files);

        if (!files || files.length <= 0) {
            return;
        }

        fileInputElement.value = "";
        disableUploadButton();

        files.forEach(file => manageFileUpload(file));
    });
};

const startPolling = () => {
    listFiles();
    pollingId = setInterval(listFiles, 2000); // 2 secs
};

const stopPolling = (pollingId) => {
    clearInterval(pollingId);
};

const manageFileUpload = (file) => {
    let xhr = new XMLHttpRequest();

    xhr.addEventListener('error', (e) => {
        showErrorAlert();
    });

    xhr.onreadystatechange = () => {
        if (xhr.readyState !== 4) {
            return;
        }

        if (xhr.readyState === 4 && xhr.status === 500) {
            showErrorAlert();
            return;
        }

        if (xhr.readyState === 4 && xhr.status === 400) {
            // TODO: Tratar erro > handleUploadError('Tamanho máximo de arquivo excedido ou extensão do arquivo inválido. Favor tentar novamente.');
            showErrorAlert();
            return;
        }

        if (xhr.readyState === 4 && xhr.status === 200) {
            let responseText = JSON.parse(xhr.responseText);
            let response = JSON.parse(responseText);

            if (!response.succeeded && response.errors !== null) {
                showErrorAlert();
            }

            setRowData(response.data);
        }
    }

    let formData = new FormData();

    formData.append('formFile', file, file.name);

    xhr.open("POST", "/home/upload");
    xhr.send(formData);
};

const listFiles = () => {
    let xhr = new XMLHttpRequest();

    xhr.onreadystatechange = () => {
        if (xhr.readyState !== 4) {
            return;
        }

        if (xhr.readyState === 4 && xhr.status === 500) {
            showErrorAlert();
            stopPolling(pollingId);
        }

        if (xhr.readyState === 4 && xhr.status === 200) {
            const responseText = JSON.parse(xhr.responseText);
            const response = JSON.parse(responseText);

            if (!response.succeeded && response.errors !== null) {
                showErrorAlert();
                stopPolling(pollingId);
            }

            clearTable();

            response.data.forEach((data) => {
                setRowData(data);
            });
        }
    };

    xhr.open('GET', '/home/files');
    xhr.send();
};

const clearTable = () => {
    while (tbodyElement.firstChild) {
        tbodyElement.removeChild(tbodyElement.firstChild);
    }
};

const setRowData = (data) => {
    let rowElement = tbodyElement.querySelector('tr[data-file-id="' + data.id + '"]');

    if (rowElement) {
        updateRow(rowElement, data);
        return;
    }

    addRow(data);
};

const addRow = (data) => {
    let clonedRow = rowCloneElement.cloneNode(true);

    updateRow(clonedRow, data);

    let nameElement = clonedRow.querySelector('[data-file-name]');

    nameElement.addEventListener("click", (e) => {
        setImageNotFoundElement();

        const fileUrl = e.target.parentElement.dataset.fileUrl;
        const fileName = e.target.parentElement.dataset.fileName;

        if (fileUrl !== "null") {
            setImageElement(fileName, fileUrl);
            offCanvasList[0].show();
        }
    });

    tbodyElement.insertRow(0).insertAdjacentElement('beforebegin', clonedRow);
    clonedRow.classList.remove('d-none');
};

const updateRow = (row, data) => {
    row.setAttribute("data-file-id", data.id);
    row.setAttribute("data-file-name", data.name);
    row.setAttribute("data-file-url", data.url);

    let nameElement = row.querySelector('[data-file-name]');
    let sizeElement = row.querySelector('[data-file-size]');
    let createdAtElement = row.querySelector('[data-file-created-at]');

    nameElement.textContent = data.name;
    sizeElement.textContent = formatSize(data.sizeInBytes);
    createdAtElement.textContent = formatToLocalDate(data.createdAt);

    if (data.processStatusId === processStatus.pending || data.processStatusId === processStatus.processing) {
        hideActionsCrudElements(row);
        showActionsProcessingElements(row);
    }

    if (data.processStatusId === processStatus.success) {
        hideActionsProcessingElements(row);
        showActionsCrudElements(row);
        enableFileDownloadAndDelete(row, data.url, data.name);
    }
};

const disableUploadButton = () => {
    buttonUploadElement.setAttribute("disabled", "disabled");
};

const enableUploadButton = () => {
    buttonUploadElement.removeAttribute("disabled", "disabled");
};

const hideActionsCrudElements = (row) => {
    let actionsCrudElement = row.querySelector('[data-file-actions-crud]');
    actionsCrudElement.classList.add("d-none");
};

const showActionsCrudElements = (row) => {
    let actionsCrudElement = row.querySelector('[data-file-actions-crud]');
    actionsCrudElement.classList.remove("d-none");
};

const hideActionsProcessingElements = (row) => {
    let actionsProcessingElement = row.querySelector('[data-file-actions-processing]');
    actionsProcessingElement.classList.add("d-none");
};

const showActionsProcessingElements = (row) => {
    let actionsProcessingElement = row.querySelector('[data-file-actions-processing]');
    actionsProcessingElement.classList.remove("d-none");
};

const enableFileDownloadAndDelete = (row, fileUrl, fileName) => {
    let downloadElement = row.querySelector('[data-file-download]');
    let deleteElement = row.querySelector('[data-file-delete]');

    downloadElement.setAttribute('onclick', `downloadFile('${fileUrl}', '${fileName}')`);
};

const downloadFile = async (fileUrl, fileName) => {
    const a = document.createElement("a");
    a.href = await toDataURL(fileUrl);
    a.download = fileName;
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
};

const toDataURL = async (url) => {
    const blob = await fetch(url).then(res => res.blob());
    return URL.createObjectURL(blob);
};

const deleteFile = () => {

};

const showErrorAlert = (message) => {
    const errorAlertMessageElement = document.getElementById('divErrorAlertMessage');

    let defaultMessage = 'Tente novamente ou contate o suporte';

    errorAlertMessageElement.innerHTML = defaultMessage;

    if (typeof message === "string" && message !== "") {
        errorAlertMessageElement.textContent = message;
    }

    errorAlertElement.show();
};

const setImageNotFoundElement = () => {
    canvasLabelElement.textContent = "Imagem não encontrada";
    canvasImageElement.src = "/image-not-found.png";
};

const setImageElement = (fileName, url) => {
    canvasLabelElement.textContent = fileName;
    canvasImageElement.src = url;
};

const formatSize = (sizeInBytes) => {
    let sizeFormatted = '0 KB';
    let kilobyte = 1024;
    let megabyte = kilobyte * kilobyte;
    let gigabyte = megabyte * kilobyte;

    if (sizeInBytes < kilobyte) {
        sizeFormatted = sizeInBytes + ' B';
        return sizeFormatted;
    }

    if (sizeInBytes < megabyte) {
        sizeFormatted = Math.trunc((sizeInBytes / kilobyte)) + ' KB';
        return sizeFormatted;
    }

    if (sizeInBytes < gigabyte) {
        sizeFormatted = (Math.trunc(sizeInBytes / (megabyte))) + ' MB';
        return sizeFormatted;
    }

    if (sizeInBytes >= gigabyte) {
        sizeFormatted = (Math.trunc(sizeInBytes / (gigabyte))) + ' GB';
        return sizeFormatted;
    }

    return sizeFormatted;
};

const formatToLocalDate = (dateString) => {
    let date = new Date(dateString);

    let year = date.getFullYear();
    let month = String(date.getMonth() + 1).padStart(2, '0');
    let day = String(date.getDate()).padStart(2, '0');
    let hour = String(date.getHours()).padStart(2, '0');
    let minute = String(date.getMinutes()).padStart(2, '0');

    return day + '/' + month + '/' + year + ' ' + hour + ':' + minute;
};

const generateGuid = () => {
    return ([1e7]+-1e3+-4e3+-8e3+-1e11).replace(/[018]/g, c =>
        (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
    );
};

configureElements();
configureEvents();
startPolling();