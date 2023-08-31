import { useEffect, useState } from "react";
import "./App.css";

function App() {
  const [data, setData] = useState<any>(null);

  useEffect(() => {
    fetchData().then((response) => {
      setData(response);
    });
  }, []);

  return (
    <>
      <h1>Hello, World!</h1>
      <p>{JSON.stringify(data)}</p>
    </>
  );
}

export default App;

export async function fetchData(): Promise<any> {
  try {
    const response = await fetch("http://localhost:5268/TaskManager/all");
    const jsonData = await response.json();
    return jsonData;
  } catch (error: any) {
    console.log(error.message);
    throw error;
  }
}
