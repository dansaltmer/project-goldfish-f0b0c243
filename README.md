# project goldfish

Created a repo to hold everything for a poc I've been wanting to try around serverless realtime patterns along with learning AWS.

Currently running at [goldfish.adonalsium.com](https://goldfish.adonalsium.com)

# Progress

- ~~Stub out UI~~
- ~~Sort some authentication, probably google login~~
  - ~~Replace google login with cognito~~
  - ~~Environment configuration~~
  - ~~Switch to access token and customise using lambda triggers ~~
- ~~Create db / rest api~~
  - Hook UI into the post message endpoint
- Figure out websockets
- Create ws api / event consumer

# Serverless Architecture

Aiming for a full serverless realtime application by using:

- Static NextJS FrontEnd hosted on S3 / CDN
- Websocket API Gateway to hold the client realtime connections
- A lambda c# minimal api to handle basic rest interaction
- A lambda c# minimal api to handle websocket connections
- A lambda c# trigger from new rows in dynamo db to trigger websocket messages to the clients.
- Cognito for user authentication and a lambda node trigger to support token generation in cognito

![image](/docs/goldfish.drawio.png)

# Client Configuration

Better ways to do this, but config will be loaded from `/config/client.json` on application load.
Each environment will provide a json file routed to S3 with that environments configuration, with local using `/frontend/public/config/client.json`.
This will keep the builds uniform across environments and not rely on environment variables.

# Local development

### AWS IAM Authentication

Authentication into AWS resources such as dynamo db during local development needs to use the AWS toolkit, make sure its installed and connected and the user has read permissions if getting errors.

# Interface

Current version is fully static and using clientside cognito authentication. UI is still stubbed out.

![image](/docs/stub-ui-screenshot.png)
