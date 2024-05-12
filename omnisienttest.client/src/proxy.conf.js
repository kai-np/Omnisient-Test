const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
    env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:5045';

const PROXY_CONFIG = [
  {
    context: [
      "/Change/calculate-change",
      "/Change/calculate-change-with-currency"
    ],
    target,
    secure: false
  }
]

module.exports = PROXY_CONFIG;
