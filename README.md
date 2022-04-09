# Hiper Products
This repository contains the code for a CRUD Application with synchronization via RabbitMQ using .NET Core and Angular.

![Products](/images/products.png)

# Run
To run the project, we're using Docker with Docker Compose. For startup use the following command:
```
docker-compose up --build -d
```
The application will be exposed on `port 8000`. The startup process take some time to boot up RabbitMQ and apply the database migrations.

# Integration
The integration between the application and the synchronization API is done with RabbitMQ. The result will be available on `port 8001`.

# Features
- [x] Product registration (CRUD)
- [x] Integration with external API for data synchronization
