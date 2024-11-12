pipeline {

    agent any

    environment {

        BACKEND_IMAGE = "backend_image"

        FRONTEND_IMAGE = "frontend_image"

    }

    stages {

        stage('Checkout') {

            steps {

                git branch: 'main', url: 'https://github.com/Raisybear/cards_against_humanity'

            }

        }  

        stage('Build') {

            parallel {

                stage('Backend Build') {

                    steps {

                        dir('Game/cards_against_humanity_backend') {

                            script {

                                echo 'Building Backend Docker Image'

                                sh 'docker build -t ${env.BACKEND_IMAGE} .'  // Baut das Docker-Image für das Backend.

                            }

                        }

                    }

                }

                stage('Frontend Build') {

                    steps {

                        dir('Game/cards_against_humanity_frontend') {

                            script {

                                echo 'Building Frontend Docker Image'

                                sh 'docker build -t ${env.FRONTEND_IMAGE} .'  // Baut das Docker-Image für das Frontend.

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

                        dir('Game/cards_against_humanity_backend') {

                            script {

                                echo 'Running Backend Tests'

                                sh 'dotnet test'  // Führt die Backend-Tests aus.

                            }

                        }

                    }

                }

                stage('Frontend Tests') {

                    steps {

                        dir('Game/cards_against_humanity_frontend') {

                            script {

                                echo 'Running Frontend Tests'

                                sh 'npm install && npm test'  // Führt die Frontend-Tests aus.

                            }

                        }

                    }

                }

            }

        }

        stage('Deploy with Docker Compose') {

            steps {

                script {

                    echo 'Deploying with Docker Compose'

                    sh 'docker-compose -f docker-compose.yml down'  // Stoppt alte Container

                    sh 'docker-compose -f docker-compose.yml up -d'  // Startet die Container im Hintergrund.

                }

            }

        }

    }
 
    post {

        always {

            echo 'Cleaning up...'

            sh 'docker-compose -f docker-compose.yml down'  // Container bereinigen

        }

        success {

            echo 'Build and Deployment completed successfully.'

        }

        failure {

            echo 'Build or Deployment failed. Please check the logs.'

        }

    }

}