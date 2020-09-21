FROM node:alpine

RUN mkdir -p /agenda-webapp
WORKDIR /agenda-webapp

COPY package.json /agenda-webapp
COPY yarn.lock /agenda-webapp

RUN yarn install

COPY . /agenda-webapp

EXPOSE 3000

CMD ["yarn", "start"]