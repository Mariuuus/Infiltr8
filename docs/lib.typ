#let color_gray = rgb(191,191,191)
#let color_blue = rgb(47,84,150)

#let game_design_document(
  title: none,
  // the game title
  authors: (),
  // list of authors
  doc
  // the rest of the doc
) = {
  set text(lang: "de", 
      region: "DE", 
      size: 11pt,
  )
  set page(paper: "a4", 
      margin: (x: 0.98in, 
      y: 0.98in, 
      top: 0.98in, 
      bottom: 0.79in)
  )
  set align(center)
  set par(justify: true)
  show link: it => underline(text(it, fill: blue.darken(30%),), offset: 1.3pt)
  show figure: set block(breakable: true)


  text([#title], size: 48pt, fill: color_gray, weight: "thin")
  v(-40pt)
  text([Game Design Document], size:11pt)
  line(length: 100%)
  v(10pt)

  authors.map(it => text([#it], fill: color_gray)).join(" | ")
  v(350pt)
  show heading.where(
  ): it => text(
    fill: color_blue,
    it,
  )
  set align(left)
  doc
}