<h1> How to run our project: </h1>

<h3>Building the database:</h3>
 cd ../database
kubectl apply -f postgres-config.yaml<br>
kubectl apply -f postgres-secret.yaml<br>
kubectl apply -f postgres-deployment.yaml<br>
kubectl apply -f postgres-service.yaml<br>
kubectl apply -f postgres-storage.yaml<br>
Building the net images:</h3>
cd ..<br>
docker build --no-cache -t gcr.io/sc-g22/net:v1 .<br>
docker push gcr.io/sc-g22/net:v1<br>
kubectl apply -f deployment.yaml<br>
kubectl apply -f service.yml<br>

<h3>Building the angular images:</h3>
change the ip for .net in services<br>
docker build --no-cache -t gcr.io/sc-g22/angular:v1 .<br>
docker push gcr.io/sc-g22/angular:v1<br>
cd ClientApp/<br>
kubectl apply -f clientapp-service.yaml<br>
kubectl apply -f clientapp-deployment.yaml<br>

<h3>Network policy yaml file:</h3>
kubectl run pod3 --image=alpine  -it -- ash <br>
wget http://34.79.215.47:8091/ [address of frontend]<br>
kubectl apply -f policy1.yaml<br>
kubectl exec pod3 -it -- ash<br>
wget http://34.79.215.47:8091/ [address of frontend]<br>

<h3>Roles and rolebinding yaml files and the owners on GCP-IAM&Admin:</h3>
kubectl apply -f blog-role.yaml<br>
kubectl apply -f blog-rolebinding.yaml<br>
kubectl auth can-i get pod --namespace default --as joyceljy6256@gmail.com (yes)<br>
kubectl auth can-i get pod --namespace default --as joyce@gmail.com (no)<br>
kubectl auth can-i create pod --namespace default --as daumantas.patapas@gmail.com (yes)<br>

<h3>Certificates</h3>
openssl req -x509 -newkey rsa:4096 -keyout key.pem -out cert.pem -days 365 -nodes<br>
kubectl create secret tls mynewsecret --key key.pem --cert cert.pem<br>

<h3>Autoscaling the application</h3>
kubectl apply -f hpa-net.yaml<br>
kubectl run -i --tty load-generator --rm --image=busybox --restart=Never -- /bin/sh -c "while sleep 0.01; do wget -q -O- http://35.205.127.220:8081/api/BlogPosts; done"<br>
cd ClientApp/<br>
kubectl apply -f hpa-angular.yaml<br>
kubectl run -i --tty load-generator --rm --image=busybox --restart=Never -- /bin/sh -c "while sleep 0.01; do wget -q -O- http://34.79.215.47:8091/; done"<br>

<h3>Run the website</h3>

<h3>Rollout:</h3>
cd ClientApp/<br>
kubectl set image deployment/angular-deployment angular-container=gcr.io/sc-g22/angular:v1  --record <br>
docker build --no-cache -t gcr.io/sc-g22/angular:v2 .     (make a new version of image)<br>
docker push gcr.io/sc-g22/angular:v2<br>
kubectl set image deployment/angular-deployment angular-container=gcr.io/sc-g22/angular:v2  --record  (change the image version in clientapp-deployment.yaml)<br>
kubectl describe deployment angular-deployment   (see the image has updated to v2)<br>
kubectl rollout history deployment/angular-deployment<br>
kubectl rollout undo deployment/angular-deployment --to-revision=   (rollback to v1)<br>
kubectl describe deployment angular-deployment (see the image has rolled back  to v1)<br>

<h3>Canary Deployment:</h3>
kubectl apply -f clientapp-deployment2.yaml<br>
kubectl scale --replicas=9 deploy angular-deployment<br>
kubectl get pods --show-labels<br>
kubectl scale --replicas=10 deploy angular-deployment-v2<br>
kubectl delete deployment angular-deployment<br>
kubectl get pods --show-labels<br>
