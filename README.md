# project goldfish

Created a repo to hold everything for a poc I've been wanting to try around serverless realtime patterns along with learning AWS.

Currently running at [chat.rak.gg](https://chat.rak.gg)

# Progress

- ~~Stub out UI~~
- ~~Sort some authentication, probably google login~~
- Create db / rest api
- Figure out websockets
- Create ws api / event consumer

# Serverless Architecture

Aiming for a full serverless realtime application by using:

- Static NextJS FrontEnd hosted on S3 / CDN
- Websocket API Gateway to hold the client realtime connections
- A lambda c# minimal api to handle basic rest interaction
- A lambda c# minimal api to handle websocket connections
- A lambda c# trigger from new rows in dynamo db to trigger websocket messages to the clients.

![image](/docs/goldfish.drawio.png)

# Interface

Current version is fully static and using clientside google login. UI is still stubbed out.

![image](/docs/stub-ui-screenshot.png)
