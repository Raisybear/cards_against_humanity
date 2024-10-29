pipeline {
    agent any

    stages {
        stage('Build') {
            steps {
                echo 'Building...'
                // Führe den Docker-Build aus
                sh 'docker build -t cards_against_humanity_image:latest .'
            }
        }

        stage('Test') {
            steps {
                echo 'Testing...'
                // Beispiel für Tests, z.B. mit Docker
                sh 'docker run --rm cards_against_humanity_image:latest pytest'
            }
        }

        stage('Deploy') {
            steps {
                echo 'Deploying...'
                // Optional: Push des Images zu einer Registry
                // sh 'docker tag mein-image:latest myregistry.com/mein-image:latest'
                // sh 'docker push myregistry.com/mein-image:latest'
            }
        }
    }

    post {
        always {
            echo 'Pipeline abgeschlossen!'
        }
    }
}
