
export function Ini(id) {
    const Charts = window.Charts.default
    const dom = document.getElementById(id)
    var chart = new Charts(dom)
    return chart
}


export function SetRingOption(id, config,chart) {
    if (chart == null) {
        chart = new Charts(dom)
    }
    chart.setOption(config, true)
}

export function GetMaxRadius(id) {
    const dom = document.getElementById(id)
    var h = dom.offsetHeight
    var w = dom.offsetWidth
    return w > h ? h/2 : w/2
}

