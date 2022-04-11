export function Filter({ q, onChange }) {
    return (
        <input class="filter" type="text" placeholder="Filter" value={q} onChange={(ev) => onChange(ev.target.value)}></input>
    )
}