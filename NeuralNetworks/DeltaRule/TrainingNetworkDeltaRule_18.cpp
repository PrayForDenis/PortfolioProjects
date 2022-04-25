#include <iostream>
#include <vector>
#include <cmath>

using namespace std;

// input data X
float X1[] = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
float X2[] = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
float X3[] = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

// targets
float D1[] = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
float D2[] = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
float D3[] = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

// output
double Result[10][3] = {{0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0},
                        {0, 0, 0}, {0, 0, 0}, {0, 0, 0}, {0, 0, 0},
                        {0, 0, 0}, {0, 0, 0}};

// weights Neural Network
float weights[3][3] = {{0, 0, 0}, {0, 0, 0}, {0, 0, 0}};

// learning koefficient (need to change if doesnt work good)
float learningRate = 0.05; // 0.05 best (now)

// count of training repeats
int epochs = 100000;

// needed accuracy
float mse = pow(10, -4);

// currentEpoch and currentMSE
int countOfEpochs = 0;
float currentMSE = 10000;

// current SumFunction values
double sum1 = 0;
double sum2 = 0;
double sum3 = 0;

// currrent ActivationFunction values 
double y1Activ = 0;
double y2Activ = 0;
double y3Activ = 0;

// current Delta values
float e1 = 0;
float e2 = 0;
float e3 = 0;

void InitializeInputData()
{
    for (int i = 0; i < 10; i++)
    {
        X1[i] = static_cast <float> (rand()) / static_cast <float> (RAND_MAX);
        X2[i] = static_cast <float> (rand()) / static_cast <float> (RAND_MAX);
        X3[i] = static_cast <float> (rand()) / static_cast <float> (RAND_MAX);
    }
}

void InitializeTargets()
{
    for (int i = 0; i < 10; i++)
    {
        D1[i] = static_cast <float> (rand()) / static_cast <float> (RAND_MAX);
        D2[i] = static_cast <float> (rand()) / static_cast <float> (RAND_MAX);
        D3[i] = static_cast <float> (rand()) / static_cast <float> (RAND_MAX);
    }
}

void InitializeWeights()
{
    for (int rows = 0; rows < 3; rows++) 
    {
        for (int cols = 0; cols < 3; cols++) 
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
    double activationResult = (exp(x) - exp(-x)) / (exp(x) + exp(-x));

    return activationResult;
}

void DeltaRule(float value_1, float value_2, float value_3, 
                float y_true_1, float y_true_2, float y_true_3,
                double* y_result_1, double* y_result_2, double* y_result_3)
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

        *y_result_1 = y1Activ;
        *y_result_2 = y2Activ;
        *y_result_3 = y3Activ;

        e1 = y_true_1 - y1Activ;
        e2 = y_true_2 - y2Activ;
        e3 = y_true_3 - y3Activ;

        currentMSE = pow(e1, 2) + pow(e2, 2) + pow(e3, 2);

        if (currentMSE > mse) 
        {
            weights[0][0] = weights[0][0] + learningRate * e1 * value_1;
            weights[1][0] = weights[1][0] + learningRate * e1 * value_2;
            weights[2][0] = weights[2][0] + learningRate * e1 * value_3;

            weights[0][1] = weights[0][1] + learningRate * e2 * value_1;
            weights[1][1] = weights[1][1] + learningRate * e2 * value_2;
            weights[2][1] = weights[2][1] + learningRate * e2 * value_3;

            weights[0][2] = weights[0][2] + learningRate * e3 * value_1;
            weights[1][2] = weights[1][2] + learningRate * e3 * value_2;
            weights[2][2] = weights[2][2] + learningRate * e3 * value_3;  
        }
        else
            return;
    }
}

void PrintResult(int iteration) 
{
    std::cout << "Значения по сетке" << endl;
    for (int j = 0; j < 3; j++)
    {
        cout << Result[iteration][j] << " "; 
    }
    cout << endl;

    std::cout << "Нужные значения" << endl;
    cout << D1[iteration] << " " << D2[iteration] << " " << D3[iteration] << endl; 

    std::cout << "Веса по сетке" << endl;
    std::cout << weights[0][0] << " " << weights[0][1] << " " << weights[0][2] << endl;
    std::cout << weights[1][0] << " " << weights[1][1] << " " << weights[1][2] << endl;
    std::cout << weights[2][0] << " " << weights[2][1] << " " << weights[2][2] << endl;

    std::cout << "Ошибка текущая" << endl;
    std::cout << currentMSE << endl;
    std::cout << "------------------------" << endl;
}

int main() 
{
    InitializeInputData();
    InitializeTargets();
    InitializeWeights();

    for (int i = 0; i < 10; i++) 
    {
        DeltaRule(X1[i], X2[i], X3[i], D1[i], D2[i], D3[i], &Result[i][0], &Result[i][1], &Result[i][2]);
        PrintResult(i);
    }
}