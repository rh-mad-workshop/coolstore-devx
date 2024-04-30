# MAD Roadshow Dev Track - InnerLoop Labs

This is a hands-on lab experience for building new cloud native applications using Red Hat OpenShift Application Runtimes (Quarkus, Spring Boot, Eclipse Vert.x and Node.js) utilizing a microservices architecture.

## CoolStore Online Store App

CoolStore is an online store web application built using Quarkus, Spring Boot, .Net, AngularJS adopting the microservices architecture.

* **Web**: Angular front-end
* **API Gateway**: Aggregates API calls to back-end services and provides a condenses REST API for front-end
* **Catalog**: a REST API for the product catalog and product information
* **Inventory**: a REST API for product's inventory status

```
                    +-------------+
                    |             |
                    |     Web     |
                    |             |
                    |  AngularJS  |
                    |             |
                    +------+------+
                          |
                          v
                    +------+------+
                    |             |
                    | API Gateway |
                    |             |
                    |    .NET     |
                    |             |
                    +------+------+
                          |
                +---------+---------+
                v                   v
          +------+------+     +------+------+
          |             |     |             |
          |   Catalog   |     |  Inventory  |
          |             |     |             |
          | Spring Boot |     |   Quarkus   |
          |             |     |             |
          +-------------+     +-------------+
```
