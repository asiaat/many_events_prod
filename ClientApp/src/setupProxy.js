const { createProxyMiddleware } = require('http-proxy-middleware');
const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:44037';

const context = [
    "/api/mevents",
    "/api/mevents/create",
    "/api/mevents/bind",
    "/api/mevents/addcompany",
    "/api/mfeetypes/feetypes",
    "/api/mfeetypes/new",
    "/api/mfeetypes/update",
    "/api/mfeetypes/delete",
    "/api/mpersons/persons",
    "/api/mpersons/create",
    "/api/mpersons/update",
    "/api/mpersons/delete",
    "/api/mcompanies/companies",
    "/api/mcompanies/create",
    "/api/mcompanies/update",
    "/api/mcompanies/delete"   
];

const onError = (err, req, resp, target) => {
    console.error(`${err.message}`);
}

module.exports = function (app) {
  const appProxy = createProxyMiddleware(context, {
    target: target,
    // Handle errors to prevent the proxy middleware from crashing when
    // the ASP NET Core webserver is unavailable
    onError: onError,
    secure: false,
    // Uncomment this line to add support for proxying websockets
    //ws: true, 
    headers: {
      Connection: 'Keep-Alive'
    }
  });

  app.use(appProxy);
};
