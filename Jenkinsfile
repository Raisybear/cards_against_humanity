pipeline {
    agent {
        kubernetes {
            cloud 'my-minikube-cloud'  // Der Name der Kubernetes-Cloud, die du oben konfiguriert hast.
            label 'minikube-agent'     // Das Label für den Agenten
            defaultContainer 'docker' // Der Container, der Docker ausführt.
        }
    }
    stages {
        stage('Checkout') {
            steps {
                git branch: 'main', url: 'https://github.com/Raisybear/cards_against_humanity.git'
            }
        }
        stage('Build Backend') {
            steps {
                script {
                    sh 'docker build -t backend_image .' // Baut das Docker-Image
                }
            }
        }
        stage('Deploy to Kubernetes') {
            steps {
                script {
                    sh 'kubectl apply -f k8s/backend-deployment.yaml' // Führt kubectl aus, um das Deployment zu erstellen.
                }
            }
        }
    }
}
