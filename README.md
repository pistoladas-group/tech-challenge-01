<h1 align="center">TechBox</h1>
<h4 align="center">Easily manage your images in an Azure Storage.</h4>

<p align="center">
  <a href="">
    <img src="https://img.shields.io/badge/version-1.0.0-blue"
         alt="version">
  </a>
  <a href="">
    <img src="https://img.shields.io/badge/license-MIT-green"
         alt="license">
  </a>
</p>


# ![techbox](.github\images\website-demo.png)


## Table of Contents

- [About](#about)
- [Built with](#built-with)
- [Browser Support](#browser-support)
- [Architecture](#architecture)
    - [Web App](#web-app)
    - [Web API](#web-api)
- [Deployment / Provision of Resource in Azure](#deployment--provision-of-resource-in-azure)


## About
This project was created to meet the Tech Challenge project of the [FIAP Technology College](https://postech.fiap.com.br/?gclid=Cj0KCQjwnf-kBhCnARIsAFlg49228y9z3y6lf_mWZEekgcxZRZBDavxtRT-zAUNs33TZOJtXpGVMNlAaAue5EALw_wcB).<br>
It consists of a web application that allow you to easily manage your image files in the Azure Storage.
It consists of a Frontend Web Application communicating with a Backend API that persists data into a SQL Server database, and handles file managing in Azure Storage.

## Built with

| Web App | API | ORM | Database
| --- | --- | --- | --- |
| [![bootstrap-version](https://img.shields.io/badge/Bootstrap-5.0.2-purple)](https://getbootstrap.com/)<br>[![fontawesome-version](https://img.shields.io/badge/Font_Awesome-6.4.0-yellow)](https://fontawesome.com/)<br>[![aspnetcore-version](https://img.shields.io/badge/ASP.NET_Core_MVC-7.0-blue)](https://learn.microsoft.com/pt-br/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-7.0)| [![aspnetcore-version](https://img.shields.io/badge/ASP.NET_Core-7.0-blue)](https://learn.microsoft.com/pt-br/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-7.0) | [![dapper-version](https://img.shields.io/badge/Dapper-2.0.138-red)](https://github.com/DapperLib/Dapper) | ![database](https://img.shields.io/badge/SQL_Server-gray)

## Browser Support

| <img src="https://user-images.githubusercontent.com/1215767/34348387-a2e64588-ea4d-11e7-8267-a43365103afe.png" alt="Chrome" width="16px" height="16px" /> Chrome | <img src="https://user-images.githubusercontent.com/1215767/34348590-250b3ca2-ea4f-11e7-9efb-da953359321f.png" alt="IE" width="16px" height="16px" /> Internet Explorer | <img src="https://user-images.githubusercontent.com/1215767/34348380-93e77ae8-ea4d-11e7-8696-9a989ddbbbf5.png" alt="Edge" width="16px" height="16px" /> Edge | <img src="https://user-images.githubusercontent.com/1215767/34348394-a981f892-ea4d-11e7-9156-d128d58386b9.png" alt="Safari" width="16px" height="16px" /> Safari | <img src="https://user-images.githubusercontent.com/1215767/34348383-9e7ed492-ea4d-11e7-910c-03b39d52f496.png" alt="Firefox" width="16px" height="16px" /> Firefox |
| :---------: | :---------: | :---------: | :---------: | :---------: |
| Yes | 11+ | Yes | Yes | Yes |


### Architecture
This is an overview of the architecture of TechBox.
# ![overview-architecture](.github\images\overview-architecture.png)

### Web App
The design of the frontend application was based on a MVC (Model View Controller) architecture, using ASP.NET Core as framework, running as a client/server, and all business logic running server-side. The application is deployed in a [Web App Azure Resource](https://azure.microsoft.com/en-us/products/app-service/web). <br>
# ![webapp-architecture](.github\images\webapp-architecture.png)

### Web API

The design of the API application was based on a REST (Representational State Transfer) and Layered Architecture, using ASP.NET Core as framework.<br> 
Although some features that could be useful in a REST API are not presented in this first version, this API can be considered RESTful according to Leonard Richardson in his book RESTful Web APIs. 2013. O'Reilly Media.

<p align="center">
  <a href="">
    <img src=".github\images\api-architecture.png" alt="api-architecture">
  </a>
</p>

## Deployment / Provision of Resource in Azure
| Web App | Web API | Storage Account | Database
| :---------: | :---------: | :---------: | :---------: |
| via Github Actions<br>(<i>push on main</i>) | via Github Actions<br>(<i>push on main</i>) | Manually | Scripts manually |
