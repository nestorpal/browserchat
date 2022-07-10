# Browser Chat

## Description

Browser Chat is a WebSocket based Chat system developed with .NET technologies. It includes a bot which attends specific commands.

## Features

- Login/Register user accounts to access the Chat Rooms
- Select one or more Chat Rooms and start sending posts
- Interact with a chatbot using the following commands:
  | Command | Description |
  | ------ | ------ |
  | **/stockcompany** | Returns a list with some of the company codes available at https://stooq.com/ |
  | **/stock=<company_code>** | Returns the quote per share of the selected company |

## Tech

Browser Chat uses the following frameworks, technologies and libraries:

- [ASP.NET Core 6.0] - For Rest API Services and MVC based web system
- [SignalR] - For WebSocket based message delivery
- [RabbitMQ] - For handling command requests between Rest API and a decoupled bot
- [MassTransit RabbitMQ] - For abstracting RabbitMQ underlying logic
- [AutoMapper] - For mapping internal structures into external models
- [MediatR] - For simplifying Rest API Controllers with mediator design pattern
- [JSON Web Token] - For restricting access and interactions with the system only for users correctly logged in
- [CSVHelper] - For parsing CSV content

## Installation

The installation is based on Kubernetes Deployment so it **requires Docker platform installed with Kubernetes enabled**.

> For detailed instructions on how to enable Kubernetes, use the following address: https://docs.docker.com/desktop/kubernetes/

After the required software is implemented go to the Install directory and run the install.bat file. This will install the following components:

| Component | Description |
| ------ | ------ |
| **RabbitMQ** | Message broker for handling bot command requests |
| **Security Service MSSQL Database** | MSSQL instance for users management |
| **Backend Service MSSQL Database** | MSSQL instance for rooms and posts storage |
| **Security Service** | Rest API Service for users management |
| **Backend Service** | Rest API Service for rooms and posts endpoints. Also works as a SignalR Hub |
| **Web Client** | User interface |
| **Bot Service** | Queue based microservice for handling command requests |

> **warning**: the installation process is based on timeouts which varies for each component so it is recommended to have a proper Internet access for the deployment to download the docker images on time.

Once the installation process finishes, you can access the Browser Chat following this address: http://localhost:30101/