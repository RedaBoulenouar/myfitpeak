import { useEffect, useState } from "react";
import { apiClient } from "../api/client";
import type { CoachSummary } from "../types/coach";

export function CoachSearchPage() {
  const [sport, setSport] = useState("");
  const [minRating, setMinRating] = useState("4");
  const [coaches, setCoaches] = useState<CoachSummary[]>([]);

  useEffect(() => {
    apiClient
      .get<CoachSummary[]>("/coaches", { params: { sport, minRating } })
      .then((response) => setCoaches(response.data))
      .catch(() => setCoaches([]));
  }, [sport, minRating]);

  return (
    <main className="page">
      <section className="toolbar">
        <input
          value={sport}
          onChange={(event) => setSport(event.target.value)}
          placeholder="Sport: fitness, yoga, boxe..."
        />
        <select value={minRating} onChange={(event) => setMinRating(event.target.value)}>
          <option value="0">Toutes les notes</option>
          <option value="3">3 etoiles et plus</option>
          <option value="4">4 etoiles et plus</option>
          <option value="5">5 etoiles</option>
        </select>
      </section>
      <section className="grid">
        {coaches.map((coach) => (
          <article className="card" key={coach.id}>
            <h2>{coach.name}</h2>
            <p>{coach.bio}</p>
            <span>{coach.sportsSpecialties}</span>
            <strong>{coach.hourlyRate} EUR / h</strong>
          </article>
        ))}
      </section>
    </main>
  );
}
