import { FormEvent, useEffect, useState } from "react";
import "./App.css";

export function App() {
  const [tasks, setTasks] = useState<any>(null);
  const [showComplete, setShowComplete] = useState<boolean>(false);
  const [taskName, setTaskName] = useState<string>("");
  const [status, setStatus] = useState<string>("");

  useEffect(() => {
    fetchTasks().then((response) => {
      setTasks(response);
    });
  }, [tasks]);

  function handleSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();

    createTask(taskName)
      .then(() => {
        setStatus(
          `Great! Your task ${taskName} has been submitted successfully.`
        );
      })
      .catch((e: any) => setStatus(e.message));
  }

  return (
    <>
      <h1>Task Manager</h1>
      <button onClick={() => setShowComplete(!showComplete)}>
        {showComplete ? "Hide" : "Show"} Complete
      </button>
      {tasks !== null
        ? tasks
            .filter((i: any) => i.isComplete === false)
            .map((i: any, j: number) => (
              <p key={j}>
                {i.name}{" "}
                <button onClick={() => completeTask(i.id)}>Complete</button>
              </p>
            ))
        : ""}
      {tasks !== null && showComplete === true
        ? tasks
            .filter((i: any) => i.isComplete === true)
            .map((i: any, j: number) => <p key={j}>{i.name}</p>)
        : ""}
      <form
        onSubmit={(e) => {
          handleSubmit(e);
        }}
      >
        <div className="sighting-field">
          <label className="create-sighting-label" htmlFor="name">
            Task Name:
          </label>
          <input onChange={(event) => setTaskName(event.target.value)} />
        </div>
        <button type="submit">Submit</button>
      </form>
    </>
  );
}

export async function fetchTasks(): Promise<any> {
  try {
    const response = await fetch("http://localhost:5268/TaskManager/all");
    const jsonData = await response.json();
    return jsonData;
  } catch (error: any) {
    console.log(error.message);
    throw error;
  }
}

export async function createTask(s: string): Promise<any> {
  const response = await fetch("http://localhost:5268/TaskManager/create", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(s),
  });

  if (!response.ok) {
    throw new Error(await response.json());
  } else {
    return response;
  }
}

export async function completeTask(taskId: number): Promise<Response> {
  const response = await fetch(
    `http://localhost:5268/TaskManager/complete/${taskId}`,
    {
      method: "PATCH",
      headers: {},
    }
  );
  if (!response.ok) {
    throw new Error(await response.json());
  } else {
    return response;
  }
}
