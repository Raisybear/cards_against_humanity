pipeline {
    agent any

    environment {
        BACKEND_IMAGE = "backend_image"
        FRONTEND_IMAGE = "frontend_image"
        K8S_NAMESPACE = "default"  // Beispiel Namespace, du kannst ihn nach Bedarf anpassen
    }

    stages {
        stage('Checkout') {
            steps {
                git branch: 'main', url: 'https://github.com/Raisybear/cards_against_humanity.git'
            }
        }   

        stage('Backend Build') {
            steps {
                dir('Game/cards_against_humanity_backend') {
                    script {
                        sh 'docker build -t ${BACKEND_IMAGE} .'  // Baut das Docker-Image für das Backend.
                    }
                }
            }
        }

        stage('Frontend Build') {
            steps {
                dir('Game/cards_against_humanity_frontend') {
                    script {
                        sh 'docker build -t ${FRONTEND_IMAGE} .'  // Baut das Docker-Image für das Frontend.
                    }
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

        stage('Deploy to Kubernetes') {
            steps {
                script {
                    // Stellen Sie sicher, dass kubectl auf dem Jenkins-Agenten verfügbar ist und auf das Cluster zugreifen kann
                    sh 'kubectl apply -f Game/k8s/mongo-deployment.yaml --namespace=${K8S_NAMESPACE}'  // MongoDB Deployment
                    sh 'kubectl apply -f Game/k8s/backend-deployment.yaml --namespace=${K8S_NAMESPACE}'  // Backend Deployment
                    sh 'kubectl apply -f Game/k8s/frontend-deployment-service.yaml --namespace=${K8S_NAMESPACE}'  // Frontend Deployment und Service
                }
            }
        }
    }

    post {
        // Entfernt den leeren always-Block, oder du kannst hier eine Aktion hinzufügen, falls du nach dem Deployment noch etwas tun möchtest
    }
}
