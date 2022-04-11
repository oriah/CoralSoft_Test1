export function SortBy({ sortBy, onChange }) {
    return (
        <select value={sortBy} onChange={(ev) => onChange(ev.target.value)}>
            <option value="Sort By">Sort By</option>
            <option value="ASC">ASC</option>
            <option value="DESC">DESC</option>
        </select>
    )
}