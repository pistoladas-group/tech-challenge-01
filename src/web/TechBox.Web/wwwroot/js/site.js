const fileInputElement = document.getElementById('formFileMultiple');

const configureEvents = () => {
    fileInputElement.onchange = function (event) {
        let files = Array.from(event.target.files);
        if (!files || files.length <= 0) {
            return;
        }
        files.forEach(file => manageFileUpload(file));
    };
};

const manageFileUpload = (file) => {
    let xhr = new XMLHttpRequest();
    let progressId = "progress-" + generateGuid();

    //disableFileUploadDoneButton();

    xhr.addEventListener("loadstart", (e) => {
        //handleUploadStart(file, progressId);
    });

    xhr.upload.addEventListener("progress", (e) => {
        //handleUploadProgress(file, e.loaded, e.total, progressId);
        console.log(file, e.loaded, e.total, progressId);
    });

    xhr.addEventListener('error', (e) => {
        //handleUploadError('Ocorreu um erro ao realizar a operação. Favor tentar novamente.');
        console.log(e);
    });

    xhr.onreadystatechange = function () {
        if (xhr.readyState !== 4) {
            return;
        }

        if (xhr.readyState === 4 && xhr.status === 500) {
            //handleUploadError('Ocorreu um erro ao realizar a operação. Favor tentar novamente.');
            console.log(500);
            return;
        }

        if (xhr.readyState === 4 && xhr.status === 400) {
            //handleUploadError('Tamanho máximo de arquivo excedido ou extensão do arquivo inválido. Favor tentar novamente.');
            console.log(400);
            return;
        }

        if (xhr.readyState === 4 && xhr.status === 200) {
            //handleUploadFinished(xhr.response);
            //updateUploadInProgressDetailsByProgressId(progressId, 100, 'Concluído');
            //enableFileUploadDoneButton();
            console.log(xhr.response);
        }
    }

    let formData = new FormData();

    formData.append('formFile', file, file.name);

    xhr.open("POST", "/home/upload");
    xhr.send(formData);
};

const generateGuid = () => {
    return ([1e7]+-1e3+-4e3+-8e3+-1e11).replace(/[018]/g, c =>
        (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
    );
};


configureEvents();