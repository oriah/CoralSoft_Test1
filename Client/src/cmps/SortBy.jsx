export function SortBy({sortBy, onChange}) {
    return (
        <select value={sortBy} onChange={onChange(this.value)}>
            <option value="Sort By">Sort By</option>
            <option value="ASC">ASC</option>
            <option value="DESC">DESC</option>
        </select>
    )
}