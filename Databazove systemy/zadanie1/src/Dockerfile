FROM alpine:3.17 as builder

RUN apk add --no-cache nodejs npm postgresql-client curl tzdata alpine-conf

RUN adduser -D dbs

WORKDIR /home/dbs

COPY package.json package-lock.json ./

RUN npm install 
COPY . .

RUN setup-timezone -z Europe/Bratislava

USER dbs

EXPOSE 8000

CMD ["npm", "run", "start", ":", "prod"], 