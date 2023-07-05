import React from 'react';
import { render, screen } from '@testing-library/react';
import App from './App';
import { BrowserRouter } from 'react-router-dom';

test('Carga de la página de login', () => {
  render(<App />, {wrapper: BrowserRouter});
  const linkElement = screen.getByRole("button");
  expect(linkElement).toBeInTheDocument();
});
