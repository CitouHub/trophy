import React from 'react';
import { Route, Routes } from 'react-router-dom';
import { Layout } from './components/layout';
import { Dashboard } from './pages/dashborad';
import { Register } from './pages/register';
import './custom.css';
import './effect.css';

export const App = () => {
    
    return (
        <Layout>
            <Routes>
                <Route index exact path='/' element={<Dashboard />} />
                <Route index exact path='/register' element={<Register />} />
            </Routes>
        </Layout>
    );
}
