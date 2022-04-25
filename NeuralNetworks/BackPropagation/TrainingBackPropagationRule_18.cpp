#include <iostream>
#include <vector>
#include <cmath>

using namespace std;

// input data X
float X1[] = {0, 0, 0, 0, 1, 1, 1, 1};
float X2[] = {0, 0, 1, 1, 0, 0, 1, 1};
float X3[] = {0, 1, 0, 1, 0, 1, 0, 1};

// targets
float D[] = {1, 1, 1, 1, 0, 0, 0, 1};

// output
double Result[] = {0, 0, 0, 0, 0, 0, 0, 0};

// weights Neural Network
float weights[3][4] = {{0, 0, 0, 0}, {0, 0, 0, 0}, {0, 0, 0, 0}};

// learning koefficient (need to change if doesnt work good)
float learningRate = 0.7; // 

// count of training repeats
int epochs = 10000;

// needed accuracy
float mse = pow(10, -3);

// currentEpoch and currentMSE
int countOfEpochs = 0;
float currentMSE = 10000;

// current SumFunction values
double sum1 = 0;
double sum2 = 0;
double sum3 = 0;
double sum4 = 0;

// currrent ActivationFunction values 
double y1Activ = 0;
double y2Activ = 0;
double y3Activ = 0;
double y4Activ = 0;

// current Delta values
double e1 = 0;
double e2 = 0;
double e3 = 0;
double e4 = 0;

void InitializeWeights()
{
    for (int rows = 0; rows < 3; rows++)
    {
        for (int cols = 0; cols < 4; cols++) 
        {
            weights[rows][cols] = static_cast <float> (rand()) / static_cast <float> (RAND_MAX);
        }   
    }
}

double Summator(float x1, float x2, float x3, float w1, float w2, float w3)
{
    double sumAnswers = x1 * w1 + x2 * w2 + x3 * w3;

    return sumAnswers;
}

double ActivationNeuron(float x)
{
    double activationResult = 1.0 / (1.0 + exp(-x));

    return activationResult;
}

void BackPropagationRule(float value_1, float value_2, float value_3, float y_true, double* y_result)
{
    while (countOfEpochs <= epochs)
    {
        countOfEpochs++;

        sum1 = Summator(value_1, value_2, value_3, weights[0][0], weights[1][0], weights[2][0]);
        sum2 = Summator(value_1, value_2, value_3, weights[0][1], weights[1][1], weights[2][1]);
        sum3 = Summator(value_1, value_2, value_3, weights[0][2], weights[1][2], weights[2][2]);

        y1Activ = ActivationNeuron(sum1);
        y2Activ = ActivationNeuron(sum2);
        y3Activ = ActivationNeuron(sum3);

        sum4 = Summator(y1Activ, y2Activ, y3Activ, weights[0][3], weights[1][3], weights[2][3]);

        y4Activ = ActivationNeuron(sum4);

        *y_result = y4Activ;

        // errors
        e4 = (y_true - y4Activ) * y4Activ * (1 - y4Activ);
        e1 = y1Activ * (1 - y1Activ) * e4 * weights[0][3];
        e2 = y2Activ * (1 - y2Activ) * e4 * weights[1][3];
        e3 = y3Activ * (1 - y3Activ) * e4 * weights[2][3];

        // last layer weights
        weights[0][3] = weights[0][3] + learningRate * e4 * y1Activ;
        weights[1][3] = weights[1][3] + learningRate * e4 * y2Activ;
        weights[2][3] = weights[2][3] + learningRate * e4 * y3Activ;

        // first layer weights
        weights[0][0] = weights[0][0] + learningRate * e1 * value_1;
        weights[1][0] = weights[1][0] + learningRate * e1 * value_2;
        weights[2][0] = weights[2][0] + learningRate * e1 * value_3;

        weights[0][1] = weights[0][1] + learningRate * e2 * value_1;
        weights[1][1] = weights[1][1] + learningRate * e2 * value_2;
        weights[2][1] = weights[2][1] + learningRate * e2 * value_3;

        weights[0][2] = weights[0][2] + learningRate * e3 * value_1;
        weights[1][2] = weights[1][2] + learningRate * e3 * value_2;
        weights[2][2] = weights[2][2] + learningRate * e3 * value_3;  

        float delta = y_true - y4Activ;
        currentMSE = pow(delta, 2);

        if (currentMSE <= mse)
            return;
    }
}

void PrintResult(int iteration) 
{
    std::cout << "Значения по сетке" << endl;
    cout << Result[iteration] << endl; 

    std::cout << "Нужные значения" << endl;
    cout << D[iteration] << endl; 

    std::cout << "Веса по сетке" << endl;
    std::cout << weights[0][0] << " " << weights[0][1] << " " << weights[0][2] << " " << weights[0][3] << endl;
    std::cout << weights[1][0] << " " << weights[1][1] << " " << weights[1][2] << " " << weights[1][3] << endl;
    std::cout << weights[2][0] << " " << weights[2][1] << " " << weights[2][2] << " " << weights[2][3] << endl;

    std::cout << "Ошибка текущая" << endl;
    std::cout << currentMSE << endl;
    std::cout << "------------------------" << endl;
}

int main() 
{
    InitializeWeights();

    for (int i = 0; i < 8; i++) 
    {
        BackPropagationRule(X1[i], X2[i], X3[i], D[i], &Result[i]);
        PrintResult(i);
    }
}