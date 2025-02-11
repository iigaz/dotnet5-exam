FROM node:latest AS builder
ARG VITE_API_URL
ENV VITE_API_URL $VITE_API_URL
WORKDIR /app
COPY xodotnet/package.json xodotnet/package-lock.json ./
RUN npm ci

COPY xodotnet .
RUN npm run build

FROM nginx:stable-alpine
COPY --from=builder /app/dist /usr/share/nginx/html
COPY nginx/nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 8080
CMD ["nginx", "-g", "daemon off;"]
