pipeline {
    agent any

    environment {
        BACKEND_IMAGE = "backend_image"
        FRONTEND_IMAGE = "frontend_image"
    }

    stages {
        stage('Checkout') {
            steps {
                git branch: 'main', url: 'https://github.com/robinsacher/cards_against_humanity'
                }
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
                    sh 'docker-compose -f docker-compose.yml up -d'  // Startet die Container im Hintergrund.
                }
            }
        }
    }

    post {
        always {
            sh 'docker-compose -f docker-compose.yml down'  // Stoppt und entfernt die Container nach Abschluss der Pipeline.
        }
    }
