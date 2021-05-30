# Lab06 Backend for Three Tier Project

Based on **Lab05 SOA Project**, adding some functionality, like *listing all users with pagination*, *getting currently logged user* and *modifying data of that user*.


# Added functionality

Added abillity to use external host URL such as *http://example.com:3000/*

Server verifies, that it's available at that host, through generating some token, which is injected into **Container** and sending it to *maybe-itself*, and if token is the same as injected, then host containing *definitely-itself*.

Also, added new routes:

- **hostcheck/** GET **!disposes**

  Provided hostcheck token

  (accepts first request, if **EXTHOST** is provided in .env, otherwise ignores).

- **api/allusers?id=** GET

  List all users.
  Accepts pagination params: *limit* and *offset*.

  *id* param to imitate login (to view email and phone of other users)

- **api/profile?id=** GET, PUT

  Get current user data.

  Update current user data.

  GET requires *id* param to imitate login
  PUT requires *id* prop in body

- **api/getroles** GET

  List all user roles.


# And as a before...

A simple use case REST API powered by **PostgreSQL**, **Node.js**, **Sequelize ORM**, **Express** written in *Typescript.*

Prequisities:

- PostgreSQL 13
- Node.js 16.x.x
- Yarn

Install project dependencies:
`yarn install`

To run in *Dev* mode:
`yarn dev`

Also you can lint code:
`yarn lint`

To build project run:
`yarn build`

Result will appear in `dist/` folder

Happy Hacking!

# About project

This project implements the SOA architecture pattern, thanks to typescript library 'typedi'.
In simple words this is the container of services. In this project we have *Sequelize models*, *Winston Logger*, and the AuthService as a services, which are injected into application container and there are accessible from any place of the code inside application.

## How to use

Available routes

- **status/** GET, HEAD 

  Healthcheck routes.

- **api/** GET

  List available API routes.

- **api/auth/** GET

  List available auth operations such as **signin** or **signup**.

- **api/auth/signin** POST

  Sign in user.
  Requires *login* and *password* in request body.

- **api/auth/signup** POST

  Register new user.

  Requires *email*, *phone*, *password*, *fullName* and *role* in request body.


\
Examples of requests:

- Using PowerShell:
![Sign in request example](misc/screen01.png)

- Using Postman:
![Sign up request example](misc/screen02.png)
