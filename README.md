
## Setup a cluster using Minikube

```
minikube start --addons=ingress
```

```
eval $(minikube docker-env)
```

### Building local images

Make sure you have ran the above commands in your terminal session before building. 

```
docker build -t test/pub-api -f pubDockerFile .
docker build -t test/sub-api -f subDockerfile .
```

## Install Dapr

Install dapr on Kubernetes
```
dapr init -k
```

Verify pods are deployed:
```
kubectl get pods -n dapr-system
```

## Helm install dependencies

Install helm from https://helm.sh/

```
helm repo add bitnami https://charts.bitnami.com/bitnami
```

```
helm repo update
```

### RabbitMQ

```
helm install rabbitmq bitnami/rabbitmq -n default --set auth.username=rabbitmq,auth.password=rabbitmq
```

## Deploy application

```
cd k8s
kubectl apply -f dapr/pubsub.yml -f DaprPub.yaml -f DaprSub.yaml
```

Verify pods are deployed:

```
$ kubectl get pods
NAME                       READY   STATUS    RESTARTS   AGE
daprpub-57dbf7846b-wjl4d   2/2     Running   0          33s
daprsub-58cc5f9d5f-4mjxs   2/2     Running   0          33s
rabbitmq-0                 1/1     Running   0          16m
```

To access the Swagger endpoints expose the URL using:
```
minikube service --url daprpub 
minikube service --url daprsub
```
