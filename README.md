# ApiGateway

- The ApiGateway is just a simple entry point. It is currently used as a swagger page for the other projects.
- The Aggregator project is responsible for the grpc calls and returning the results(json).
- The NewsApiConsumer is a very simple implementation for this https://newsapi.org/

# Local Development configurations
During local development, when opening the ApiGateway in a browser, this message may appear `You can't visit localhost right now because the website sent scrambled credentials that Microsoft Edge can't process.`. I found the simplest solution to this here https://stackoverflow.com/questions/66552431/microsoft-edge-forcing-https-and-refusing-a-self-signed-certificate, you justa have to click anywhere within the browser window and type: `thisisunsafe`.

> [!WARNING]
> (these projects are all under development, many improvements are pending)
