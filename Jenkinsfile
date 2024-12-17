pipeline {
    agent {
        kubernetes {
            label 'minikube-agent'  // Das Label des Kubernetes-Agenten, das du in Jenkins konfiguriert hast
            defaultContainer 'kubectl'  // Der Standardcontainer für kubectl
            yaml '''
apiVersion: v1
kind: Pod
metadata:
  labels:
    some-label: my-pod
spec:
  containers:
    - name: kubectl
      image: lachlanevenson/k8s-kubectl  // Container mit kubectl, um mit Kubernetes zu interagieren
      command:
        - cat
      tty: true
    - name: docker
      image: docker:19.03.12  // Docker-Container für das Bauen von Docker-Images
      command:
        - cat
      tty: true
  serviceAccountName: default
'''
        }
    }

    environment {
        BACKEND_IMAGE = "backend_image"
        FRONTEND_IMAGE = "frontend_image"
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

        stage('Deploy with Docker Compose') {
            steps {
                script {
                    // Deploy mit docker-compose auf Kubernetes
                    sh 'docker-compose -f docker-compose.yml up -d'  // Startet die Container im Hintergrund.
                }
            }
        }
    }

    post {
        always {
            // Stoppt und entfernt die Container nach Abschluss der Pipeline
            sh 'docker-compose -f docker-compose.yml down'
        }
    }
}
