import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import { BrowserRouter, Routes, Route } from "react-router"
import  Soon from './Pages/Soon.jsx'
import Home from './Pages/Home.jsx'

createRoot(document.getElementById('root')).render(
  <BrowserRouter>
    <Routes>
      <Route path="/*" element={<Home />} />
      <Route path="/soon" element={<Soon />} />
    </Routes>
  </BrowserRouter>
)
