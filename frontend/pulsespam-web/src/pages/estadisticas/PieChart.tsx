import { Box, Button, Card, CardActions, CardContent, Typography } from "@mui/material";
import { useState } from "react";
import { Respuesta } from "../../types/Respuesta";
import { Pie } from "react-chartjs-2";
import React from 'react';
import { Chart as ChartJS, ArcElement, Tooltip, Legend, Title } from 'chart.js';

type PieChartProps = {
    labels: string[],
    label: string,
    data: number[],
    height: string,
    width: string
};

function PieChart(props: PieChartProps) {
  ChartJS.register(ArcElement, Tooltip, Legend, Title);

    const dataPie = {
        labels: props.labels,
        datasets: [
            {
              label: props.label,
              data: props.data,
              backgroundColor: [
                'rgba(255, 99, 132, 0.2)',
                'rgba(54, 162, 235, 0.2)',
                'rgba(255, 206, 86, 0.2)',
                'rgba(75, 192, 192, 0.2)',
                'rgba(153, 102, 255, 0.2)',
                'rgba(255, 159, 64, 0.2)',
              ],
              borderColor: [
                'rgba(255, 99, 132, 1)',
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(75, 192, 192, 1)',
                'rgba(153, 102, 255, 1)',
                'rgba(255, 159, 64, 1)',
              ],
              borderWidth: 1,
            },
          ],
    }

    return <Pie data={dataPie} height={props.height} width={props.width} options={{
      plugins: {
        title: {
          display: true,
          text: "Desglose de puntuaciones",
          align: "center",
          padding: {
            top: 10,
            bottom: 20,
          },
        }
      }, maintainAspectRatio: false
    }}/>;
}

export default PieChart;