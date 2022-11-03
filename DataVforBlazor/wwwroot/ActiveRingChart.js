var chart=null
export function SetRingOption(id, config) {
    const Charts = window.Charts.default
    const dom = document.getElementById(id)
    if (chart == null) {
        chart = new Charts(dom)
    }
    console.log(config)
    chart.setOption(config, true)
}

export function GetMaxRadius(id) {
    const dom = document.getElementById(id)
    var h = dom.offsetHeight
    var w = dom.offsetWidth
    return w > h ? h/2 : w/2
}

