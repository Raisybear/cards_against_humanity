pipeline {
    agent any

    environment {
        BACKEND_IMAGE = "backend_image"
        FRONTEND_IMAGE = "frontend_image"
    }

    stages {
        stage('Checkout') {
            steps {
                git branch: 'main', 
                    url: 'https://github.com/robinsacher/cards_against_humanity'
            }
        }

        stage('Backend Build') {
            steps {
                dir('Game/cards_against_humanity_backend') {
                    script {
                        sh 'docker build -t ${BACKEND_IMAGE} .'  // Builds the Docker image for the backend.
                    }
                }
            }
        }

        stage('Frontend Build') {
            steps {
                dir('Game/cards_against_humanity_frontend') {
                    script {
                        sh 'docker build -t ${FRONTEND_IMAGE} .'  // Builds the Docker image for the frontend.
                    }
                }
            }
        }

        stage('Run Tests') {
            steps {
                dir('Game/cards_against_humanity_backend') {
                    sh 'dotnet test'  // Runs backend tests.
                }
                dir('Game/cards_against_humanity_frontend') {
                    sh 'npm install && npm test'  // Runs frontend tests.
                }
            }
        }

        stage('Deploy with Docker Compose') {
            steps {
                script {
                    sh 'docker-compose -f docker-compose.yml up -d'  // Starts containers in detached mode.
                }
            }
        }
    }

    post {
        always {
            sh 'docker-compose -f docker-compose.yml down'  // Stops and removes containers after pipeline completion.
        }
    }
}
