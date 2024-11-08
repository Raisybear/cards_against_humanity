pipeline {
    agent any
    stages {
        stage('Checkout') {
            steps {
                git branch: 'main', 
                    url: 'https://github.com/robinsacher/cards_against_humanity'
            }
        }
    }
}
