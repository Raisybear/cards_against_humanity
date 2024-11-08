pipeline {
    agent any
        tools {
        git 'git'  // Nutzt das Git-Tool von Jenkins
    }
    stages {
        stage('Checkout') {
            steps {
                git branch: 'main', 
                    url: 'https://github.com/robinsacher/cards_against_humanity.git'
            }
        }
    }
}
