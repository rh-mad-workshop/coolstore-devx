# Cloud Native Roadshow Labs  [![Build Status](https://travis-ci.org/openshift-labs/cloud-native-labs.svg?branch=ocp-3.11)](https://travis-ci.org/openshift-labs/cloud-native-labs)

This is a one-day hands-on lab experience for building Cloud Native applications using 
Red Hat OpenShift Application Runtimes (Quarkus, Spring Boot, Eclipse Vert.x and Node.js) 
utilizing a microservices architecture.


## CoolStore Online Store App

CoolStore is an online store web application built using Quarkus, Spring Boot, Eclipse Vert.x,
Node.js and AngularJS adopting the microservices architecture.

* **Web**: A Node.js/Angular front-end
* **API Gateway**: aggregates API calls to back-end services and provides a condenses REST API for front-end
* **Catalog**: a REST API for the product catalog and product information
* **Inventory**: a REST API for product's inventory status

```
                    +-------------+
                    |             |
                    |     Web     |
                    |             |
                    |   Node.js   |
                    |  AngularJS  |
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
