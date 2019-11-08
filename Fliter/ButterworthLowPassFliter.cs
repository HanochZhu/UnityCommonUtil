/**
 * 巴特沃夫低通滤波器
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterworthLowPassFliter 
{
    #region private region

    float[] fliter_param = new float[5];
    float fliter_output;
    int fliter_select;
    int last_fliter_hz = 0;
    float[] last_input_param = new float[2]{ 0.0f, 0.0f };
    float[] last_output_param = new float[2] { 0.0f, 0.0f };

    // 
    float[,] butterworth_lowpass_param_1000 = new float[4,5]{{0.00362f,0.00724f,0.00362f,1.82270f,-0.83720f},   //1000_20hz lowpass fliter
											                  {0.00554f,0.01108f,0.00554f,1.77863f,-0.80080f},   //1000_25hz lowpass fliter
											                  {0.00782f,0.01564f,0.00782f,1.73472f,-0.76601f},   //1000_30hz lowpass fliter
                                                              {0.01043f,0.02086f,0.01043f,1.69100f,-0.73273f}};  //1000_35hz lowpass fliter
                                             
    float[,] butterworth_lowpass_param_800 = new float[4,5] {{0.00554f,0.01108f,0.00554f,1.77863f,-0.80080f},     //800_20hz lowpass fliter
                                                            {0.00844f,0.01688f,0.00844f,1.72378f,-0.75755f},     //800_25hz lowpass fliter
                                                            {0.01186f,0.02372f,0.01186f,1.66920f,-0.71663f},     //800_30hz lowpass fliter
                                                            {0.01575f,0.03150f,0.01575f,1.61494f,-0.67794f}};    //800_35hz lowpass fliter
    #endregion

    float ButterworthLowpassFliter(float rawinput, int frequency, int fliterhz)
    {
        if (last_fliter_hz != fliterhz)
        {
            if (fliterhz == 20)
            {
                fliter_select = 0;
            }
            else if (fliterhz == 25)
            {
                fliter_select = 1;
            }
            else if (fliterhz == 30)
            {
                fliter_select = 2;
            }
            else
            {
                fliter_select = 3;
            }
            for (int i = 0; i < 5; i++)
            {
                if (frequency == 800)
                {
                    fliter_param[i] = butterworth_lowpass_param_800[fliter_select,i];
                }
                else
                {
                    fliter_param[i] = butterworth_lowpass_param_1000[fliter_select,i];
                }
            }
        }

        last_fliter_hz = fliterhz;

        fliter_output = fliter_param[0] * rawinput + fliter_param[1] * last_input_param[0] + fliter_param[2] * last_input_param[1]
                        + fliter_param[3] * last_output_param[0] + fliter_param[4] * last_output_param[1];
        last_input_param[1] = last_input_param[0];
        last_input_param[0] = rawinput;
        last_output_param[1] = last_output_param[0];
        last_output_param[0] = fliter_output;

        return fliter_output;
    }
}
