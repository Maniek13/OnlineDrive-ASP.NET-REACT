import React from 'react';
import './styles/global.css';
import Explorer from './Explorer';
import { createRoot } from 'react-dom/client';

const domNode = document.getElementById('root');
const root = createRoot(domNode);

root.render(
  <React.StrictMode>
    <Explorer />
  </React.StrictMode>
);

