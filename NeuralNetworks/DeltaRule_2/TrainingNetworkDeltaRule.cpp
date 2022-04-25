#include <iostream>
#include <vector>
#include <cmath>

using namespace std;

// input data X and Y
int X1[] = {0, 0, 1, 1};
int X2[] = {0, 1, 0, 1};
int D1[] = {0, 0, 0, 1};
int D2[] = {0, 1, 1, 1};

// output
float Result[4][2] = {{0, 0}, {0, 0}, {0, 0}, {0, 0}};

// weights Neural Network
float weights[2][2] = {{0, 0}, {0, 0}};

// koefficient y = k*x
float k = 0.6;

// learning koefficient (need to change if doesnt work good)
float learningRate = 0.05; // 0.05 best (now)

// count of training repeats
int epochs = 10000;

// needed accuracy
float mse = pow(10, -3);

// currentEpoch and currentMSE
int countOfEpochs = 0;
float currentMSE = 10000;

// current SumFunction values
float sum1 = 0;
float sum2 = 0;

// currrent ActivationFunction values 
float y1Activ = 0;
float y2Activ = 0;

// current Delta values
float e1 = 0;
float e2 = 0;

void InitializeWeights()
{
    for (int rows = 0; rows < 2; rows++) 
    {
        for (int cols = 0; cols < 2; cols++) 
        {
            weights[rows][cols] = static_cast <float> (rand()) / static_cast <float> (RAND_MAX);
        }   
    }
}

double Summator(int x1, int x2, float w1, float w2)
{
    float sumAnswers = x1 * w1 + x2 * w2;

    return sumAnswers;
}

double ActivationNeuron(float x)
{
    float activationResult = k * x;

    return activationResult;
}

void DeltaRule(float value_1, float value_2, float y_true_1, float y_true_2, float* y_result_1, float* y_result_2)
{
    while (countOfEpochs <= epochs)
    {
        countOfEpochs++;

        sum1 = Summator(value_1, value_2, weights[0][0], weights[1][0]);
        sum2 = Summator(value_1, value_2, weights[0][1], weights[1][1]);

        y1Activ = ActivationNeuron(sum1);
        y2Activ = ActivationNeuron(sum2);

        *y_result_1 = y1Activ;
        *y_result_2 = y2Activ;

        e1 = y_true_1 - y1Activ;
        e2 = y_true_2 - y2Activ;

        currentMSE = pow(e1, 2) + pow(e2, 2);

        if (currentMSE > mse) 
        {
            weights[0][0] = weights[0][0] + learningRate * e1 * value_1;
            weights[1][0] = weights[1][0] + learningRate * e1 * value_2;

            weights[0][1] = weights[0][1] + learningRate * e2 * value_1;
            weights[1][1] = weights[1][1] + learningRate * e2 * value_2;
        }
        else
            return;
    }
}

void PrintResult(int iteration) 
{
    std::cout << "Значения по сетке" << endl;
    for (int j = 0; j < 2; j++)
    {
        cout << Result[iteration][j] << " "; 
    }
    cout << endl;

    std::cout << "Нужные значения" << endl;
    cout << D1[iteration] << " " << D2[iteration] << endl; 

    std::cout << "Веса по сетке" << endl;
    std::cout << weights[0][0] << " " << weights[0][1] << endl;
    std::cout << weights[1][0] << " " << weights[1][1] << endl;

    std::cout << "Ошибка текущая" << endl;
    std::cout << currentMSE << endl;
    std::cout << "------------------------" << endl;
}

int main() 
{
    InitializeWeights();
    
    for (int i = 0; i < 4; i++)
    {
        DeltaRule(X1[i], X2[i], D1[i], D2[i], &Result[i][0], &Result[i][1]);
        PrintResult(i);
    }
}