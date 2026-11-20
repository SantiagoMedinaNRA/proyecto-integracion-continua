// ======================================================================
// Pipeline Declarativo - Potter Academy CI/CD
// Stages: Checkout, Build, Test, Post
// ======================================================================

pipeline {
    agent any
    
    options {
        buildDiscarder(logRotator(numToKeepStr: '10'))
        timeout(time: 10, unit: 'MINUTES')
        timestamps()
    }
    
    environment {
        API_IMAGE = "potter-api:${env.BUILD_NUMBER}"
        WEB_IMAGE = "potter-web:${env.BUILD_NUMBER}"
    }
    
    stages {
        stage('Checkout') {
            steps {
                echo "[CHECKOUT] Starting code download..."
                checkout scm
                sh 'git log --oneline -1'
                echo "[CHECKOUT] Successfully downloaded"
            }
        }
        
        stage('Build') {
            steps {
                echo "[BUILD] Building Docker images..."
                script {
                    sh '''
                        echo "[BUILD] Building Api image: ${API_IMAGE}"
                        docker build -f Api/Dockerfile -t ${API_IMAGE} .
                        echo "[BUILD] Api image built successfully"
                        
                        echo "[BUILD] Building Web image: ${WEB_IMAGE}"
                        docker build -f Web/Dockerfile -t ${WEB_IMAGE} .
                        echo "[BUILD] Web image built successfully"
                    '''
                }
            }
        }
        
        stage('Test') {
            steps {
                echo "[TEST UNIT] Running unit tests..."
                sh '''
                    echo "[TEST UNIT] Testing Api..."
                    dotnet test Tests/Api.UnitTests -c Release --verbosity minimal
                    
                    echo "[TEST UNIT] Testing Web..."
                    dotnet test Tests/Web.UnitTests -c Release --verbosity minimal
                '''
                
                echo "[TEST INTEGRATION] Starting integration tests..."
                sh '''
                    echo "[TEST INTEGRATION] Bringing up docker compose..."
                    docker compose up -d
                    
                    echo "[TEST INTEGRATION] Waiting for services..."
                    sleep 5
                    
                    echo "[TEST INTEGRATION] Testing Api endpoint..."
                    curl -f http://localhost:5000/api/characters?page=1 > /dev/null
                    
                    if [ $? -eq 0 ]; then
                        echo "[TEST INTEGRATION] Api endpoint OK"
                    else
                        echo "[TEST INTEGRATION] Api endpoint FAILED"
                        exit 1
                    fi
                '''
            }
        }
    }
    
    post {
        always {
            echo "[CLEANUP] Cleaning up resources..."
            sh '''
                docker compose down 2>/dev/null || true
                echo "[CLEANUP] Docker compose cleaned up"
            '''
        }
        
        success {
            echo "[SUCCESS] Build ${env.BUILD_NUMBER} PASSED"
            echo "[SUCCESS] Api Image: ${env.API_IMAGE}"
            echo "[SUCCESS] Web Image: ${env.WEB_IMAGE}"
        }
        
        failure {
            echo "[FAILURE] Build ${env.BUILD_NUMBER} FAILED"
            echo "[FAILURE] Check the log above for details"
        }
    }
}
