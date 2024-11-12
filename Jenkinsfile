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

        stage('Build') {

            parallel {

                stage('Backend Build') {

                    steps {

                        dir('cards_against_humanity-main/Game/cards_against_humanity_backend') {

                            script {

                                echo 'Starting Backend Build Stage'

                                try {

                                    sh 'docker build -t ${env.BACKEND_IMAGE} .'  // Baut das Docker-Image für das Backend.

                                    echo 'Backend Docker Image Built Successfully'

                                } catch (Exception e) {

                                    echo "Error during Backend Build: ${e.message}"

                                    throw e

                                }

                            }

                        }

                    }

                }

                stage('Frontend Build') {

                    steps {

                        dir('cards_against_humanity-main/Game/cards_against_humanity_frontend') {

                            script {

                                echo 'Starting Frontend Build Stage'

                                try {

                                    sh 'docker build -t ${env.FRONTEND_IMAGE} .'  // Baut das Docker-Image für das Frontend.

                                    echo 'Frontend Docker Image Built Successfully'

                                } catch (Exception e) {

                                    echo "Error during Frontend Build: ${e.message}"

                                    throw e

                                }

                            }

                        }

                    }

                }

            }

        }

        stage('Run Tests') {

            parallel {

                stage('Backend Tests') {

                    steps {

                        dir('cards_against_humanity-main/Game/cards_against_humanity_backend') {

                            script {

                                echo 'Starting Backend Tests'

                                try {

                                    sh 'dotnet test'  // Führt die Backend-Tests aus.

                                    echo 'Backend Tests Completed Successfully'

                                } catch (Exception e) {

                                    echo "Error during Backend Tests: ${e.message}"

                                    throw e

                                }

                            }

                        }

                    }

                }

                stage('Frontend Tests') {

                    steps {

                        dir('cards_against_humanity-main/Game/cards_against_humanity_frontend') {

                            script {

                                echo 'Starting Frontend Tests'

                                try {

                                    sh 'npm install && npm test'  // Führt die Frontend-Tests aus.

                                    echo 'Frontend Tests Completed Successfully'

                                } catch (Exception e) {

                                    echo "Error during Frontend Tests: ${e.message}"

                                    throw e

                                }

                            }

                        }

                    }

                }

            }

        }

        stage('Deploy with Docker Compose') {

            steps {

                dir('cards_against_humanity-main') {

                    script {

                        echo 'Starting Deployment with Docker Compose'

                        try {

                            sh 'docker-compose -f docker-compose.yml down'  // Stoppt alte Container

                            echo 'Old Containers Stopped Successfully'

                            sh 'docker-compose -f docker-compose.yml up -d'  // Startet die Container im Hintergrund.

                            echo 'Deployment Completed Successfully'

                        } catch (Exception e) {

                            echo "Error during Deployment: ${e.message}"

                            throw e

                        }

                    }

                }

            }

        }

    }
 
    post {

        always {

            echo 'Cleaning up...'

            dir('cards_against_humanity-main') {

                try {

                    sh 'docker-compose -f docker-compose.yml down'  // Container bereinigen

                    echo 'Cleanup Completed Successfully'

                } catch (Exception e) {

                    echo "Error during Cleanup: ${e.message}"

                }

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