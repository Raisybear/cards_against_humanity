pipeline {
    agent any  // Der Agent, der für den Pipeline-Ausführungsprozess verwendet wird.

    environment {
        BACKEND_IMAGE = "backend_image"  // Umgebungsvariable für das Backend-Image.
        FRONTEND_IMAGE = "frontend_image"  // Umgebungsvariable für das Frontend-Image.
    }

    stages {
        stage('Checkout') {  // Erste Phase, um den Code aus dem Git-Repository zu klonen.
            steps {
                sshagent(['github-ssh-key']) { // ID der SSH-Zugangsdaten, die du in Schritt 3 erstellt hast.
                    sh 'git clone git@github.com:robinsacher/cards_against_humanity.git'
                }
            }
        }

        stage('Backend Build') {  // Phase, um das Backend zu bauen.
            steps {
                dir('backend') {  // Wechselt in das Verzeichnis des Backends.
                    script {
                        sh 'docker build -t ${BACKEND_IMAGE} .'  // Baut das Docker-Image für das Backend.
                    }
                }
            }
        }

        stage('Frontend Build') {  // Phase, um das Frontend zu bauen.
            steps {
                dir('frontend') {  // Wechselt in das Verzeichnis des Frontends.
                    script {
                        sh 'docker build -t ${FRONTEND_IMAGE} .'  // Baut das Docker-Image für das Frontend.
                    }
                }
            }
        }

        stage('Run Tests') {  // Phase, um Tests für Backend und Frontend auszuführen.
            steps {
                dir('backend') {
                    sh 'dotnet test'  // Führt die Backend-Tests aus.
                }
                dir('frontend') {
                    sh 'npm install && npm test'  // Führt die Frontend-Tests aus.
                }
            }
        }

        stage('Deploy with Docker Compose') {  // Phase für das Deployment mit Docker Compose.
            steps {
                script {
                    sh 'docker-compose -f docker-compose.yml up -d'  // Startet die Container im Hintergrund.
                }
            }
        }
    }

    post {
        always {
            sh 'docker-compose -f docker-compose.yml down'  // Stoppt und entfernt die Container immer nach Abschluss der Pipeline.
        }
    }
}
