pipeline {
    agent any

    environment {
        BACKEND_IMAGE = "robinsacher/cards_against_humanity_backend:latest"
        FRONTEND_IMAGE = "robinsacher/cards_against_humanity_frontend:latest"
    }

    stages {
        stage('Checkout') {
            steps {
                git branch: 'main', url: 'https://github.com/Raisybear/cards_against_humanity.git'
            }
        }

        stage('Build Backend Docker Image') {
            steps {
                dir('Game/cards_against_humanity_backend') {
                    script {
                        // Docker Image bauen, um es in Kubernetes zu verwenden
                        sh 'docker build -t ${BACKEND_IMAGE} .'
                    }
                }
            }
        }

        stage('Build Frontend Docker Image') {
            steps {
                dir('Game/cards_against_humanity_frontend') {
                    script {
                        // Docker Image bauen, um es in Kubernetes zu verwenden
                        sh 'docker build -t ${FRONTEND_IMAGE} .'
                    }
                }
            }
        }

        stage('Push Backend Docker Image') {
            steps {
                script {
                    // Push das Backend Docker Image ins Registry
                    sh 'docker push ${BACKEND_IMAGE}'
                }
            }
        }

        stage('Push Frontend Docker Image') {
            steps {
                script {
                    // Push das Frontend Docker Image ins Registry
                    sh 'docker push ${FRONTEND_IMAGE}'
                }
            }
        }

        stage('Deploy to Kubernetes') {
            steps {
                script {
                    // Stelle sicher, dass kubectl auf den richtigen Kubernetes-Cluster zugreift
                    sh 'kubectl apply -f k8s/backend-deployment.yaml'
                    sh 'kubectl apply -f k8s/frontend-deployment.yaml'
                    sh 'kubectl apply -f k8s/mongo-deployment.yaml'
                    sh 'kubectl apply -f k8s/backend-service.yaml'
                    sh 'kubectl apply -f k8s/frontend-service.yaml'
                    sh 'kubectl apply -f k8s/mongo-service.yaml'
                }
            }
        }

        stage('Run Tests') {
            steps {
                // Tests im Backend-Verzeichnis ausführen
                dir('Game/cards_against_humanity_backend') {
                    sh 'dotnet test'  // Führt die Backend-Tests aus.
                }
                // Tests im Frontend-Verzeichnis ausführen
                dir('Game/cards_against_humanity_frontend') {
                    sh 'npm install && npm test'  // Führt die Frontend-Tests aus.
                }
            }
        }
    }

    post {
        always {
            // Optional: Kubernetes-Ressourcen nach Abschluss der Pipeline löschen
            sh 'kubectl delete -f k8s/backend-deployment.yaml'
            sh 'kubectl delete -f k8s/frontend-deployment.yaml'
            sh 'kubectl delete -f k8s/mongo-deployment.yaml'
            sh 'kubectl delete -f k8s/backend-service.yaml'
            sh 'kubectl delete -f k8s/frontend-service.yaml'
            sh 'kubectl delete -f k8s/mongo-service.yaml'
        }
    }
}
