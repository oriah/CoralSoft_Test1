export function Filter({q, onChange}) {
    return (
        <input type="text" placeholder="Filter" value={q} onChange={onChange(this.value)}></input>
    )
}