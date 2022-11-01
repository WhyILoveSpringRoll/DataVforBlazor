export function Test(id,config,shape) {
    const { CRender, GRAPHS } = window.CRender

    const canvas = document.getElementById(id)
    const render = new CRender(canvas)
    const [w, h] = render.area
    const position = [w / 2, h / 2]
    if (config.textAlign === 'left') position[0] = 0
    if (config.textAlign === 'right') position[0] = w
    console.log(config)
    var myshape = {
        number: config.number,
        content: config.text,
        toFixed: config.toFixed,
        position: position,
        rowGap: config.rowGap,
        formatter: undefined
    }
    var mystyle = {
        textAlign: config.textAlign,
        textBaseline: 'middle',
        fontSize: config.fontSize,
        fill: config.fill,
    }

    const graph = new GRAPHS.Text({
        animationCurve: config.animationCurve,
        animationFrame: config.animationFrame,
        shape: myshape,
        style: mystyle
    })
    render.add(graph)
    mystyle.opacity = 1;
    graph.animation('style', mystyle, true)
    graph.animation('shape', myshape)

}

