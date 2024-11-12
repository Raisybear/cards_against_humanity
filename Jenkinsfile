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

                dir('cards_against_humanity-main/Game/cards_against_humanity_backend') {

                    echo 'Starting Backend Build Stage'

                    sh 'docker build -t ${BACKEND_IMAGE} .'  // Baut das Docker-Image für das Backend.

                    echo 'Backend Docker Image Built Successfully'

                }

            }

        }

        stage('Frontend Build') {

            steps {

                dir('cards_against_humanity-main/Game/cards_against_humanity_frontend') {

                    echo 'Starting Frontend Build Stage'

                    sh 'docker build -t ${FRONTEND_IMAGE} .'  // Baut das Docker-Image für das Frontend.

                    echo 'Frontend Docker Image Built Successfully'

                }

            }

        }

        stage('Run Backend Tests') {

            steps {

                dir('cards_against_humanity-main/Game/cards_against_humanity_backend') {

                    echo 'Starting Backend Tests'

                    sh 'dotnet test'  // Führt die Backend-Tests aus.

                    echo 'Backend Tests Completed Successfully'

                }

            }

        }

        stage('Run Frontend Tests') {

            steps {

                dir('cards_against_humanity-main/Game/cards_against_humanity_frontend') {

                    echo 'Starting Frontend Tests'

                    sh 'npm install && npm test'  // Führt die Frontend-Tests aus.

                    echo 'Frontend Tests Completed Successfully'

                }

            }

        }

        stage('Deploy with Docker Compose') {

            steps {

                dir('cards_against_humanity-main') {

                    echo 'Starting Deployment with Docker Compose'

                    sh 'docker-compose -f docker-compose.yml down'  // Stoppt alte Container

                    echo 'Old Containers Stopped Successfully'

                    sh 'docker-compose -f docker-compose.yml up -d'  // Startet die Container im Hintergrund.

                    echo 'Deployment Completed Successfully'

                }

            }

        }

    }
 
    post {

        always {

            echo 'Cleaning up...'

            dir('cards_against_humanity-main') {

                sh 'docker-compose -f docker-compose.yml down'  // Container bereinigen

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
