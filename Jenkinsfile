pipeline {
    agent any

    environment {
        BACKEND_IMAGE = "backend_image"  // Name des Backend-Images
        FRONTEND_IMAGE = "frontend_image" // Name des Frontend-Images
    }

    stages {
        stage('Backend Build') {
            steps {
                dir('backend') {
                    script {
                        // Backend Image bauen
                        sh 'docker build -t ${BACKEND_IMAGE} .'
                    }
                }
            }
        }

        stage('Frontend Build') {
            steps {
                dir('frontend') {
                    script {
                        // Frontend Image bauen
                        sh 'docker build -t ${FRONTEND_IMAGE} .'
                    }
                }
            }
        }

        stage('Run Tests') {
            steps {
                // Beispielhafte Tests für Backend und Frontend
                dir('backend') {
                    sh 'dotnet test'
                }
                dir('frontend') {
                    sh 'npm install && npm test'
                }
            }
        }

        stage('Deploy with Docker Compose') {
            steps {
                script {
                    // Docker Compose Stack starten
                    sh 'docker-compose -f docker-compose.yml up -d'
                }
            }
        }
    }

    post {
        always {
            // Container nach Abschluss der Pipeline herunterfahren
            sh 'docker-compose -f docker-compose.yml down'
        }
    }
}
