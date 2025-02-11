import classes from "./gamesList.module.css";
import GameInfoCard, { GameInfoCardProps } from "./gameInfoCard.tsx";
import { useCallback, useEffect, useRef, useState } from "react";
import api from "../../config/axios.ts";

function GamesList(props: GamesListProps) {
  const [games, setGames] = useState<GameInfoCardProps[]>(props.content);
  const [page, setPage] = useState<number>(props.page);
  const [maxPage, setMaxPage] = useState<number>(props.maxPage);
  const [loading, setLoading] = useState(false);
  const observerRef = useRef<HTMLDivElement | null>(null);

  const fetchGames = useCallback(async () => {
    if (loading || (maxPage !== null && page > maxPage)) return;

    setLoading(true);
    try {
      const response = await api.get(`/games?page=${page}&pagesize=20`);

      setGames((prev) => [...prev, ...response.data.content]);
      setMaxPage(response.data.maxPage);
      setPage((prev) => prev + 1);
    } catch (error) {
      console.error("Error fetching games:", error);
    } finally {
      setLoading(false);
    }
  }, [page, maxPage, loading]);

  useEffect(() => {
    if (loading) return;
    const observer = new IntersectionObserver(
      (entries) => {
        if (entries[0].isIntersecting) fetchGames();
      },
      { threshold: 1.0 },
    );

    if (observerRef.current) observer.observe(observerRef.current);
    return () => observer.disconnect();
  }, [fetchGames, loading]);

  return (
    <div className={classes.listWrapper}>
      {games.map((infoCard: GameInfoCardProps, i: number) => (
        <GameInfoCard {...infoCard} key={i} />
      ))}
      <div ref={observerRef} style={{ height: "1px" }} />
    </div>
  );
}

export default GamesList;

export interface GamesListProps {
  page: number;
  maxPage: number;
  content: Array<GameInfoCardProps>;
}
