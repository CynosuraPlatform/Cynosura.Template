const gulp = require('gulp');
const zip = require('gulp-zip');
const xslt = require('gulp-xslt');
const rename = require('gulp-rename');
const uuidv1 = require('uuid/v1');
const clean = require('gulp-clean');
const psmdcp = uuidv1().replace(/\-/g, '');

gulp.task('clean', ['dist-clean', 'content-clean']);

gulp.task('dist-clean',
    () =>
        gulp.src('./dist/')
            .pipe(clean())
);

gulp.task('content-clean',
    () =>
        gulp.src('./content/')
            .pipe(clean())
);

gulp.task('content-copy',
    () =>
        gulp.src(['../src/**/*', '!../src/**/node_modules/**', '!../src/.vs/**', '!../src/.idea/**',], { dot: true })
            .pipe(gulp.dest('./content'))
);

gulp.task('template-copy',
    () =>
        gulp.src('template.json')
            .pipe(gulp.dest('./content/.template.config'))
);

gulp.task('prepare-content', ['content-copy', 'template-copy'],
    () => gulp.src('./content/**', { dot: true })
        .pipe(gulp.dest('./dist/content')));

gulp.task('prepare-nuspec',
    () => gulp.src('./template.nuspec')
        .pipe(rename('Cynosura.Template.nuspec'))
        .pipe(gulp.dest('./dist')));

gulp.task('prepare-psmdcp',
    () => gulp.src('./template.nuspec')
        .pipe(xslt('./psmdcp.xsl'))
        .pipe(rename(`${psmdcp}.psmdcp`))
        .pipe(gulp.dest('./dist/package/services/metadata/core-properties')));

gulp.task('prepare', ['prepare-content', 'prepare-nuspec', 'prepare-psmdcp']);



gulp.task('pack', ['prepare'],
    () => gulp.src(['./dist/**/*', '././base/**'], { dot: true })
        .pipe(zip('./basic.nupkg', { compress: true }))
        .pipe(rename('Cynosura.Template.nupkg'))
        .pipe(gulp.dest('./artifacts')));

gulp.task('default', ['clean'],
    () => gulp.start('pack'));
