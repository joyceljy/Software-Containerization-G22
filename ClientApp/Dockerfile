# Stage 1: Compile and Build angular codebase

# Use official node image as the base image
FROM node:16 as build

# Set the working directory
WORKDIR /ClientApp

# Add the source code to app
COPY . /ClientApp

COPY package*.json /ClientApp/

# Install all the dependencies
RUN npm install

ARG configuration=production
# Generate the build of the application
RUN npm run build -- --outputPath=./dist/out --configuration $configuration


# Stage 2: Serve app with nginx server

# Use official nginx image as the base image
FROM nginx:latest

# Copy the build output to replace the default nginx contents.
COPY --from=build /ClientApp/dist/ /usr/share/nginx/html

# Copy nginx configuration files from host to installation

COPY /nginx-custom.conf /etc/nginx/conf.d/default.conf

# Expose port 80
EXPOSE 80