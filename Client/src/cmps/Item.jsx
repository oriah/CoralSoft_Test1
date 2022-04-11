export function Item({item}) {
    return (
        <div className="item">
            <img src={item.url} alt=""/>
            <h5>{item.title}</h5>
        </div>
    )
}