pipeline {

    agent any
 
    environment {

        BACKEND_IMAGE = "backend_image"

        FRONTEND_IMAGE = "frontend_image"

    }
 
    stages {

        stage('Checkout') {

            steps {

                echo 'Starting Checkout Stage'

                git branch: 'main', url: 'https://github.com/Raisybear/cards_against_humanity'

                echo 'Checkout Stage Completed'

            }

        }
 
        stage('Backend Build') {

            steps {

                echo 'Entering Backend Build Directory'

                dir('cards_against_humanity-main/Game/cards_against_humanity_backend') {

                    echo 'Starting Backend Build Stage'

                    sh 'docker build -t ${BACKEND_IMAGE} .'

                    echo 'Backend Docker Image Built Successfully'

                }

            }

        }
 
        stage('Frontend Build') {

            steps {

                echo 'Entering Frontend Build Directory'

                dir('cards_against_humanity-main/Game/cards_against_humanity_frontend') {

                    echo 'Starting Frontend Build Stage'

                    sh 'docker build -t ${FRONTEND_IMAGE} .'

                    echo 'Frontend Docker Image Built Successfully'

                }

            }

        }
 
        stage('Run Backend Tests') {

            steps {

                echo 'Entering Backend Test Directory'

                dir('cards_against_humanity-main/Game/cards_against_humanity_backend') {

                    echo 'Starting Backend Tests'

                    sh 'dotnet test'

                    echo 'Backend Tests Completed Successfully'

                }

            }

        }
 
        stage('Run Frontend Tests') {

            steps {

                echo 'Entering Frontend Test Directory'

                dir('cards_against_humanity-main/Game/cards_against_humanity_frontend') {

                    echo 'Starting Frontend Tests'

                    sh 'npm install && npm test'

                    echo 'Frontend Tests Completed Successfully'

                }

            }

        }
 
        stage('Deploy with Docker Compose') {

            steps {

                echo 'Entering Deployment Directory'

                dir('cards_against_humanity-main') {

                    echo 'Starting Deployment with Docker Compose'

                    sh 'docker-compose -f docker-compose.yml down || true'  // Stoppt alte Container (keine Fehler, falls nicht vorhanden)

                    echo 'Old Containers Stopped Successfully'

                    sh 'docker-compose -f docker-compose.yml up -d'

                    echo 'Deployment Completed Successfully'

                }

            }

        }

    }
 
    post {

        always {

            echo 'Starting Cleanup...'

            dir('cards_against_humanity-main') {

                sh 'docker-compose -f docker-compose.yml down || true'  // Container bereinigen (keine Fehler, falls nicht vorhanden)

                echo 'Cleanup Completed Successfully'

            }

        }

        success {

            echo 'Build and Deployment completed successfully.'

        }

        failure {

            echo 'Build or Deployment failed. Please check the logs.'

        }

    }

}