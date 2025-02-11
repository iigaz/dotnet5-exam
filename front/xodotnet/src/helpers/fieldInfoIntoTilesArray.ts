export default function fieldInfoIntoTiles(fieldState: string) {
  return fieldState
    .split("")
    .map((char) => (char === "-" ? null : char === "x" ? "x" : "o"));
}
