import { MainPage } from '../../features/main/MainPage';
import './App.css';
import { Toaster } from 'react-hot-toast';

function App() {
  return (
    <div className="App">
      <Toaster />
      <MainPage />
    </div>
  );
}

export default App;
